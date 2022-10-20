<template>
  <v-container fluid fill-height class="d-flex justify-center">
    <v-img
      class="mr-4"
      src="https://www.telexpress.com/Images/resource/banner_telligent.jpg"
      max-width="900"
    ></v-img>
        <H1>FAST</H1>
    <v-card v-if="$store.state.status.step==0" min-width="400" class="pa-10">
      <v-card-title class="d-flex justify-center">
        <v-img src="https://www.telexpress.com/images/logo_c.svg" max-width="300"></v-img>
      </v-card-title>
      <v-card-text class="text-center">
        <p class="text-h5 text--primary">會員登入 / 註冊</p>
        <v-form ref="form" lazy-validation>
          <v-text-field
            v-model="phoneNumber.input"
            v-if="$store.state.status.registerType==2"
            outlined
            autofocus
            :label="inputLabel"
            prepend-inner-icon="mdi-cellphone"
            :rules="[rules.required(phoneNumber.input)]"
            @keydown.enter.prevent="accountChecked"
          ></v-text-field>
          <v-text-field
            v-model="account.account"
            v-if="$store.state.status.registerType==0||$store.state.status.registerType==1&&$store.state.status.pageType==0"
            outlined
            autofocus
            :label="inputLabel"
            prepend-inner-icon="mdi-email"
            :rules="[rules.required(account.account)]"
            @keydown.enter.prevent="accountChecked"
          ></v-text-field>
          <v-btn
            block
            large
            class="primary my-2"
            @click="accountChecked"
            @keyup.enter="accountChecked"
          >下一步</v-btn>
          <a
            v-if="$store.state.status.step==1&&$store.state.status.registerStatus==1"
            @click="$store.commit('setStep',5)"
          >忘記密碼</a>
        </v-form>
      </v-card-text>
      <ThirdParty />
      <v-card-actions></v-card-actions>
    </v-card>
    <Verification :phoneNumber="phoneNumber" v-if="$store.state.status.step==1" />
    <Register
      v-if="!$store.state.status.registerStatus&&$store.state.status.step==3"
      :requiredValue="requiredValue"
      :account="account"
    />
  </v-container>
</template>

<script>
import { getAccountexisted, postQuickLoginAsync } from "@/apis/authApi";
import rules from "@/plugins/rules";
import Register from "@/components/Register.vue";
export default {
  data: function() {
    return {
      btnStatus: false,
      email: "",
      account: {},
      rules: rules,
      /**registerType 帳戶輸入方法 0:通用(手機/email),1:email,2:手機 */
      registerType: this.$store.state.status.registerType,
      /**loginType 登入方法 0:快速登入,1:一般登入*/
      loginType: this.$store.state.status.loginType,
      /**pageType 0:輸入帳號(email),1:輸入手機*/
      pageType: this.$store.state.status.pageType,
      /**step 0:Home,1:SMS,2:RegisterPassword,3:RegisterUserInfo,4:login,5:ForgotPassword*/
      step: this.$store.state.status.step,
      /**registerStatus 0:false,1:true*/
      registerStatus: this.$store.state.status.registerStatus,
      companyId: "33104D76-30BE-4E36-95F8-7CD5A4BA6190",
      channelId: "0efa73ee-9ad5-11ec-a1e2-42010a29f03c",
      phoneNumber: {
        input: null, //  raw data
        number: "", //substr
        valid: false,
        countryCode: "", //
        captchaKey: "", // countryCode+number
        formattedNumber: "" //(+countryCode)+number
      },
      verifyCode: null,
      phone: null,
      userInfo: {},
      message: "",
      requiredValue: {
        profiles: [
          {
            name: "name",
            isRequired: true,
            isEditable: true,
            id: "af33a3b0-d00d-11ec-8c74-0242ac170003"
          },
          {
            name: "email",
            isRequired: true,
            isEditable: true,
            id: "af33a3b0-d00d-11ec-8c74-0242ac170004"
          },
          {
            name: "mobile",
            isRequired: true,
            isEditable: true,
            id: "af33a3b0-d00d-11ec-8c74-0242ac170005"
          }
        ],
        customProfiles: [
          {
            name: "ShoppingShabit",
            title: "購物習慣",
            isRequired: false,
            isEditable: true,
            componentType: 1,
            items: [
              {
                value: "網路購物"
              },
              {
                value: "直播拍賣"
              },
              {
                value: "實體通路購物"
              },
              {
                value: "型錄購物"
              },
              {
                value: "電視購物"
              }
            ],
            id: "e3f2302a-2d50-11ea-911a-42010a29f003"
          }
        ]
      },
      password: ""
    };
  },
  created() {
    if (this.$route.query.channelId !== null) {
      // this.init();
      // this.channelSetting();
    }
  },
  methods: {
    async accountExisted() {
      this.setAccountInfo();
      console.log("accountExisted");
      let resp = await getAccountexisted(this.account);
      if (resp.data == false && resp.status == 200) {
        console.log("false");
        this.$store.commit("setRegisterStatus", 0);
      } else if (resp.status == 200) {
        console.log("200U");
        this.$store.commit("setRegisterStatus", 1);
        this.$store.commit("setSMSType", 2);
      }
    },
    async accountChecked() {
      const TWRegular = /^09[0-9]{8}$/;
      const SGPRegular = /^[89][0-9]{7}$/;
      const MORegular = /^6[0-9]{7}$/;
      if (
        TWRegular.test(this.phoneNumber.input || this.account.account) ||
        SGPRegular.test(this.phoneNumber.input || this.account.account) ||
        MORegular.test(this.phoneNumber.input || this.account.account)
      ) {
        console.log("testPhone");
        if (!this.phoneNumber.input) {
          this.phoneNumber.input = this.account.account;
        }

        if (this.registerType == 0 || this.registerType == 2) {
          this.accountExisted();
        }
        if (TWRegular.test(this.phoneNumber.input || this.account.account)) {
          this.phoneNumber.number = this.phoneNumber.input
            .replace(/\+/, "")
            .replace(/^0/, "");
          this.phoneNumber.countryCode = 886;
          this.phoneNumber.formattedNumber =
            "+(" + this.phoneNumber.countryCode + ")" + this.phoneNumber.number;
          this.phoneNumber.captchaKey =
            this.phoneNumber.countryCode + this.phoneNumber.number;
        } else if (
          SGPRegular.test(this.phoneNumber.input || this.account.account)
        ) {
          this.phoneNumber.countryCode = 65;
          this.phoneNumber.number = this.phoneNumber.input;
          this.phoneNumber.formattedNumber =
            "+(" + this.phoneNumber.countryCode + ")" + this.phoneNumber.input;
          this.phoneNumber.captchaKey =
            this.phoneNumber.countryCode + this.phoneNumber.input;
        } else if (
          MORegular.test(this.phoneNumber.input || this.account.account)
        ) {
          this.phoneNumber.countryCode = 853;
          this.phoneNumber.number = this.phoneNumber.input;
          this.phoneNumber.formattedNumber =
            "+(" + this.phoneNumber.countryCode + ")" + this.phoneNumber.input;
          this.phoneNumber.captchaKey =
            this.phoneNumber.countryCode + this.phoneNumber.input;
        }
        if (this.$store.state.status.registerStatus == 0) {
          this.$store.commit("setSMSType", 3);
        } else if (this.$store.state.status.registerStatus == 1) {
          let account = {
            companyId: this.$store.state.companyId,
            userId: this.account.account,
            returnUrl: ""
          };
          let resp = await postQuickLoginAsync(account);
          console.log(resp);
        }
        this.$store.commit("setStep", 1);
      } else if (
        /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(
          this.account.account
        )
      ) {
        console.log("email");
        this.accountExisted();
        this.$store.commit("setPageType", 1);
        this.$store.commit("setRegisterType", 2);
        this.$refs.form.reset();
      }
    },

    phoneNumberChecked(formattedNumber, { number, valid, country }) {
      if (valid) console.log("number", number);
      this.phoneNumber.input = number.input;
      this.phoneNumber.formattedNumber = number.significant;
      this.phoneNumber.valid = valid;
      this.phoneNumber.countryCode = country.dialCode;
      console.log("country", country);
    },
    porgotPassword() {
      this.$store.commit("setSMSStep", 1);
      this.$store.commit("setStep", 5);
      this.$forceUpadte();
    },
    updateAccountInfo(password) {
      this.account.password = password;
    },
    setAccountInfo() {
      if (this.registerType == 2) {
        this.account.account = this.phoneNumber.input;
      }
      this.registerType == 2 ? this.phoneNumber.input : this.account.account;
      this.account.companyId = this.$store.state.companyId;
      this.account.accountType = this.registerType;
    }
  },
  components: {
    Verification: () => import("@/components/Verification"),
    ThirdParty: () => import("@/components/ThirdParty"),
    Register: () => import("@/components/Register"),
    Password: () => import("@/components/Password")
  },
  computed: {
    phoneMessage() {
      if (!this.phoneNumber.valid && this.phoneNumber.input.length > 0) {
        console.log("length");
        this.message = this.$t("phone.error");
        return this.message;
      }
    },
    inputLabel() {
      if (this.$store.state.status.registerType == 0) {
        return this.$t("placeholder.normal");
      } else if (this.$store.state.status.registerType == 1) {
        return this.$t("placeholder.email");
      } else if (this.$store.state.status.registerType == 2) {
        return this.$t("placeholder.phone");
      }
    }
  }
};
</script>
<style scoped>
.v-text-field .v-input__control .v-input__slot {
  min-height: 40px !important;
  display: flex !important;
  align-items: center !important;
}
</style>
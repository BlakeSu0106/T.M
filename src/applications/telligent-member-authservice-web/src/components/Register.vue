<template>
  <v-card-text>
    <p class="text-h5 text--primary text-center">
      <strong>{{$t('register.title')}}</strong>
    </p>
    <v-form>
      <div v-if="profileList.includes('name')">
        <label class="text-left" style="color:red">*</label>
        <label class="text-left">{{$t('label.name')}}</label>
        <v-text-field v-model="userInfo.name" inline-block height="32px" outlined hide-details></v-text-field>
      </div>
      <div v-if="profileList.includes('gender')">
        <label style="color:red">*</label>
        <label>{{$t('label.gender')}}</label>
        <v-chip-group v-model="gender" column>
          {{gender}}
          <v-chip value="0" filter outlined>{{$t('label.male')}}</v-chip>
          <v-chip value="1" filter outlined>{{$t('label.female')}}</v-chip>
        </v-chip-group>
      </div>
      <div v-if="profileList.includes('nickname')">
        <label class="text-left" style="color:red">*</label>
        <label class="text-left">{{$t('label.nickname')}}</label>
        <v-text-field v-model="userInfo.nickname" inline-block height="32px" outlined hide-details></v-text-field>
      </div>
      <div v-if="profileList.includes('email')">
        <label class="text-left" style="color:red">*</label>
        <label class="text-left">{{$t('label.email')}}</label>
        <v-text-field v-model="userInfo.email" inline-block height="32px" outlined hide-details></v-text-field>
      </div>
      <div v-if="profileList.includes('mobile')">
        <label class="text-left" style="color:red">*</label>
        <label class="text-left">{{$t('label.mobile')}}</label>
        <v-text-field v-model="userInfo.mobile" inline-block height="32px" outlined hide-details></v-text-field>
      </div>
      <div v-if="profileList.includes('birth')">
        <label style="color:red">*</label>
        <label>生日</label>
        <v-menu
          ref="menu1"
          v-model="menu1"
          :close-on-content-click="false"
          transition="scale-transition"
          offset-y
          min-width="auto"
        >
          <template v-slot:activator="{ on, attrs }">
            <v-text-field
              outlined
              v-model="userInfo.birth"
              label="Date"
              prepend-inner-icon="mdi-calendar"
              v-bind="attrs"
              v-on="on"
            ></v-text-field>
          </template>
          <v-date-picker v-model="date" no-title @input="menu1 = false"></v-date-picker>
        </v-menu>
      </div>
      <Address v-if="profileList.includes('address')" class="ma-3" />
      <div v-for="(component,index) in customProfiles" :key="component.id">
        <label class="text-left" style="color:red">*</label>
        <label class="text-left">{{component.title}}</label>
        <v-select
          v-if="component.componentType==1"
          v-model="selectedCustoms[index]"
          class="my-2"
          :items="component.items"
          item-text="value"
          item-value="value"
          multiple
          outlined
          hide-details
          small-chips
          @change="getCustomProfile(component.id,index)"
        ></v-select>
        <v-chip-group
          v-model="userInfo.customProfiles[index]"
          column
          v-if="component.componentType==0"
        >
          <v-chip
            :value="item.value"
            v-for="item in component.items"
            :key="item.value"
            filter
            outlined
          >{{item.value}}</v-chip>
        </v-chip-group>
        <v-select
          v-model="userInfo.customProfiles[index]"
          v-if="component.componentType==2"
          class="my-2"
          :items="component.items"
          item-text="value"
          item-value="value"
          outlined
          hide-details
          small-chips
        ></v-select>
        <v-textarea
          outlined
          v-if="component.componentType==3"
          :label="component.title"
          :value="userInfo.customProfiles[index]"
          hint="Hint text"
        ></v-textarea>
      </div>
      <v-btn block large class="primary my-5" @click="userRegisted">下一步</v-btn>
    </v-form>
  </v-card-text>
</template>

<script>
import { postUserRegisterAsync, postLoginAsync } from "@/apis/authApi";
import { getRequiredAsync } from "@/apis/profileApi";
export default {
  data: function() {
    return {
      userInfo: {
        //        "name": null,
        // "email": null,
        // "mobile": null,
        // "phone": null,
        // "countryCode": null,
        // "countryCallingCode": null,
        // "areaCallingCode": null,
        // "birth": "2022-05-19T19:47:01.378Z",
        // "gender": 0,
        // "personalId": null,
        // "zipCode": null,
        // "country": null,
        // "province": null,
        // "city": null,
        // "district": null,
        // "address": null,
        // "avatar": null,
        // "remark": null,
        customProfiles: []
      },
      date: new Date(Date.now() - new Date().getTimezoneOffset() * 60000)
        .toISOString()
        .substr(0, 10),
      dateFormatted: this.formatDate(
        new Date(Date.now() - new Date().getTimezoneOffset() * 60000)
          .toISOString()
          .substr(0, 10)
      ),
      menu1: false,
      gender: "",
      zipCode: "",
      address: "",
      requireList: [],
      profileList: [],
      customProfiles: [],
      selectedCustoms: []
    };
  },
  async created() {
    this.account.channelId = this.$store.state.channelInfo.channelId;
    if (this.account && this.$store.state.status.registerType !== 2) {
      this.userInfo.email = this.account.account;
      this.userInfo.mobile = this.phoneNumber.input;
    } else if (this.$store.state.status.registerType == 2) {
      // this.account.channelId = this.$store.state.channelInfo.channelId;
      this.account.userId = this.userInfo.mobile;
      this.userInfo.mobile = this.account.account;
      //   else if (this.$store.state.status.registerType == 2) {
      //   this.userInfo.mobile = this.account.account;
      // this.account.uuid = this.userInfo.mobile;

      // }
    }
    await this.getRequired();
  },
  props: {
    phoneNumber: Object,
    account: Object
  },
  computed: {
    computedDateFormatted() {
      return this.formatDate(this.date);
    }
  },
  methods: {
    formatDate(date) {
      if (!date) return null;

      const [year, month, day] = date.split("-");
      return `${month}/${day}/${year}`;
    },
    parseDate(date) {
      if (!date) return null;

      const [month, day, year] = date.split("/");
      return `${year}-${month.padStart(2, "0")}-${day.padStart(2, "0")}`;
    },
    async userRegisted() {
      // if (this.userInfo.customProfiles.length > 0) {
      //   this.userInfo.customProfiles.forEach((element, index) => {});
      // }
      // await postUserRegisterAsync(this.userInfo)
      /**true */
      //     let accountInfo={
      // "companyId": this.$store.state.channelInfo.companyId,
      // "userId": this.account.userId,
      // "password": this.account.password,
      // "returnUrl": "string"
      //     }
      this.userInfo.account = this.account;
      this.userInfo.account.uuid = this.phoneNumber.number;
      try {
        let resp = await postUserRegisterAsync(this.userInfo);
        if (resp.data == true) {
          // await postLogin(accountInfo);
        } else if (resp.data == fasle && resp.status == 200) {
        }
      } catch (error) {}

      this.$store.state("setStep", 4);
    },
    async getRequired() {
      let resp = await getRequiredAsync(
        this.$store.state.companyId,
        this.$store.state.channelInfo.channelId
      );
      this.requireList = resp.data;
      this.profileList = resp.data.profiles.map(item => Object.values(item)[0]);
      this.customProfiles = resp.data.customProfiles;
    },
    getCustomProfile(customProfileId, index) {
      let tmp = this.selectedCustoms[index];
      this.userInfo.customProfiles[index] = { id: customProfileId };
      this.userInfo.customProfiles[index].items = tmp;
    }
  },

  watch: {
    date(val) {
      this.dateFormatted = this.formatDate(this.date);
      this.userInfo.birth = this.formatDate(this.date);
    }
  },
  components: {
    Address: () => import("@/components/Address")
  }
};
</script>
<style scoped>
::v-deep.v-select__selections input {
  display: none;
  width: 60px;
}
::v-deep.v-text-field .v-input__control .v-input__slot {
  min-height: 36px !important;
  display: flex !important;
  align-items: center !important;
}
::v-deep.phone-field-input {
  width: 100px;
}
</style>



        
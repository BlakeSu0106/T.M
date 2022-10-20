<template>
  <div class="d-flex justify-center flex-column">
    <v-form ref="form" v-model="valid" lazy-validation>
        <div class="row">    <v-select
      v-model="selectedCountry"
      :items="fcountryList"
      item-value="code"
      item-text="country"
      hide-details
      dense
      outlined
      width="100px"
    ></v-select>
    <v-btn color="blue-grey" outlined disabled @click="selectedStatus=true" >
      {{'+'+selectedCountry}}
      <v-icon right>mdi-menu-down</v-icon>
    </v-btn>
    <v-text-field disabled class="phone-field-input" height="32px" outlined v-model="phone" hide-details></v-text-field></div>
    </v-form>
  </div>
</template>
<script>
import countryCode from "@/assets/data/countrycode";
export default {
  data: function() {
    return {
            date: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
    dateFormatted: this.formatDate((new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)),
    menu1: false,
    menu2: false,
      selectedCountry: 886,
      selectedStatus: false,
      countryList: [],
      fcountryList: [],
      phone: "988340591",
      TWRegular: /^09[0-9]{8}$/,
      items: [
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
      ]
    };
  },
  created() {
    this.countryList = countryCode;
    this.countryCode(this.countryList);
  },
  computed: {
    phoneNumber() {
      if (this.TWRegular.test(this.phone)) {
        this.phone = this.phone.replace(/\+/, "").replace(/^0/, "");
        return this.phone;
      } else {
        return phone;
      }
    },
    computedDateFormatted() {
      return this.formatDate(this.date);
    }
  },
  methods: {
    async countryCode(list) {
      this.fcountryList = list.map(items => ({
        code: items.code,
        country: this.$t(items.country) + " " + "+" + items.code
      }));
      this.countryList = list.map(items => ({
        code: "+" + items.code
      }));
    },

    formatDate(date) {
      if (!date) return null;

      const [year, month, day] = date.split("-");
      return `${month}/${day}/${year}`;
    },
    parseDate(date) {
      if (!date) return null;

      const [month, day, year] = date.split("/");
      return `${year}-${month.padStart(2, "0")}-${day.padStart(2, "0")}`;
    }
  },

  watch: {
          date (val) {
      this.dateFormatted = this.formatDate(this.date)
    },
  }
};
</script>
<style>
/* .v-select__selections input {
  display: none;
}
.v-text-field .v-input__control .v-input__slot {
  min-height: 36px !important;
  display: flex !important;
  align-items: center !important;
}
 .phone-field-input{
      width: 100px;
} */
</style>
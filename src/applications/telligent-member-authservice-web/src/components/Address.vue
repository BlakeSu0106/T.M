<template>
  <div class="d-flex justify-center flex-column">
    <!-- <v-form ref="form" lazy-validation>
      <v-row>
        <v-col md-4>
          <v-select
            class="my-2"
            v-model="country"
            :items="countryCodeList"
            item-value="countryCode"
            item-text="countryName"
            hide-details
            prepend-inner-icon="mdi-earth"
            outlined
            @change="getCountryCode"
          ></v-select>
        </v-col>
        <v-col md-4>
          <v-select
            class="my-2"
            v-model="city"
            :items="area"
            item-value="CityName"
            item-text="CityName"
            hide-details
            prepend-inner-icon="mdi-home-city-outline"
            outlined
            return-object
          ></v-select>
        </v-col>
        <v-col md-4>
          <v-select
            class="my-2"
            v-model="district"
            :items="city.AreaList"
            item-value="AreaName"
            item-text="AreaName"
            hide-details
            outlined
          >
            <template v-slot:selection="{item}">{{item.ZipCode}}-{{ item.AreaName }}</template>
          </v-select>
        </v-col>
      </v-row>
      <v-row>
        <v-text-field class="my-2" outlined label="地址" prepend-inner-icon="mdi-map-marker"></v-text-field>
      </v-row>
    </v-form>-->
    <div class="d-flex justify-center flex-column">
      <v-form ref="form" lazy-validation>
        <div class="row">
          <v-select
            class="fit"
            v-model="country"
            :items="countryCodeList"
            item-value="countryCode"
            item-text="countryName"
            hide-details
            dense
            outlined
            label="國家"
            :append-icon="false"
          ></v-select>
          <v-select
            v-if="country=='TW'"
            v-model="city"
            class="fit"
            :append-icon="false"
            :items="area"
            label="縣市"
            item-value="CityName"
            item-text="CityName"
            hide-details
            dense
            outlined
            return-object
          ></v-select>
          <v-select
            v-if="country=='TW'"
            class="fit"
            :append-icon="false"
            v-model="district"
            label="區域"
            :items="city.AreaList"
            item-value="AreaName"
            item-text="AreaName"
            hide-details
            dense
            outlined
            return-object
          ></v-select>
          <v-btn
            min-height="40"
            v-if="country=='TW'"
            color="blue-grey"
            outlined
            disabled
          >{{district.ZipCode}}</v-btn>

          <v-text-field
            v-if="country=='TW'"
            class="my-2"
            label="地址"
            outlined
            prepend-inner-icon="mdi-map-marker"
          ></v-text-field>
        </div>
        <v-row>
          <v-text-field
            v-if="country!=='TW'"
            class="my-2"
            label="地址"
            outlined
            prepend-inner-icon="mdi-map-marker"
          ></v-text-field>
        </v-row>
        <!-- <v-text-field v-model="zipCode" label="郵遞區號" style="padding:0px !important" outlined></v-text-field> -->
        <!-- <v-text-field class="my-2" label="地址" outlined prepend-inner-icon="mdi-map-marker"></v-text-field> -->
      </v-form>
    </div>
  </div>
</template>
<script>
import countryCode from "@/assets/data/countrycode.json";
import areacode from "@/assets/data/areacode.json";
export default {
  data: function() {
    return {
      countryCodeList: countryCode.country,
      country: "",
      areacodeList: areacode.city,
      city: "",
      district: ""
    };
  },
  created() {},
  computed: {
    area() {
      if (this.country == "TW") {
        this.areaCodeList = this.areacodeList;
      } else {
        this.areaCodeList = [{}];
      }
      return this.areaCodeList;
    },
    cityCode() {
      _.filter(this.areaCodeList, { cityName: this.areaCode });
    }
  },
  methods: {
    getCountryCode(countryCode) {
      console.log(countryCode);
    }
  },

  watch: {
    date(val) {
      this.dateFormatted = this.formatDate(this.date);
    }
  }
};
</script>
<style scoped>
.v-select.fit {
  width: min-content;
}
.v-select.fit .v-select__selection--comma {
  text-overflow: unset;
}
.v-text-field--outlined.v-input--dense .v-label {
  top: 0px !important;
}
::v-deep.v-text-field .v-input__control .v-input__slot {
  min-height: 36px !important;
  display: flex !important;
  align-items: center !important;
}
</style>
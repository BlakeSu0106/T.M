import Vue from 'vue'
import Vuex from 'vuex'

Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    userInfo: {
      phoneNumber:{}
    },
    thirdPartyChannelInfo:{},
    companyId:null,
    channelInfo:{},
    verifyCode:null,
    status:{
      registerType:null,      /**registerType=accountType 帳戶輸入方法 0:通用(手機/email),1:email,2:手機 */
      loginType:null,      /**loginType 登入方法 0:快速登入,1:一般登入*/
      registerStatus:0,      /**registerStatus 0:未註冊,1:已註冊*/
      pageType:0,      /**pageType 0:輸入帳號(email),1:輸入手機*/
      step:0,      /**step 0:Home,1:SMS,2:RegisterPassword,3:RegisterUserInfo,4:login,5:ForgotPassword*/
      SMSType:null      /** 0驗證後登入 1驗證後重設密碼 2驗證後輸入密碼*/
    }
  },
  getters: {
  },
  mutations: {
    setUser(state, userProfile) {
      state.userInfo.phoneNumber = userProfile.phoneNumber;
    },
    setChannel(state,channel){
      state.channelInfo=channel;
      state.status.loginType=channel.loginType;
      state.status.registerType=channel.registerType;
    },
    setVerifyCode(state,verifyCode){
      state.verifyCode=verifyCode;
    },
    setRegisterType(state,registerType){
     state.status.registerType=registerType;
    },
    setRegisterStatus(state,registerStatus){
     state.status.registerStatus=registerStatus;
    }
    ,
    setPageType(state,pageType){
     state.status.pageType=pageType;
    }
    ,
    setRegisterType(state,registerType){
     state.status.registerType=registerType;
    }
    ,
    setStep(state,step){
     state.status.step=step;
    }
    ,
    setSMSType(state,SMSType){
      state.status.SMSType=SMSType;
    },
    setCompanyId(state,companyId){
      state.companyId=companyId;
    },
    setThirdPartyChannelInfo(state,thirdPartyChannelInfo){
      state.thirdPartyChannelInfo=thirdPartyChannelInfo;
    }
  },
  actions: {
  },
  modules: {
  }
})

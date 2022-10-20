import Vue from 'vue';
import VueI18n from 'vue-i18n';
import zhCn from './locales/zh-cn'
import zhTw from './locales/zh-tw'

const messages = {
  'zh-tw': zhTw,
  'zh-cn': zhCn
};

Vue.use(VueI18n);
export default new VueI18n({
  locale: 'zh-tw',
  messages,
  silentTranslationWarn: true
});
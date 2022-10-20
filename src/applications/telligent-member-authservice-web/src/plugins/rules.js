import i18n from './i18n';

const rules = {
  required(value) {
      if (value === 0) {
        value = value.toString();
      }
      return value ? true : i18n.t('rules.required');
    
  },
  lessThan(value, length) {
    return value
      ? value.length <= length ||
      i18n.t('rules.lessthan').replace('${length}', length)
      : true;
  },
  validateEmail(value) {
    return (
      /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(value) ||
      i18n.t('rules.email')
    );
  },
  validateSame(value, value2) {
    return value === value2 ? true : i18n.t('rules.confirmPassword');
  },
};

export default rules;

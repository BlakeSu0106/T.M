const mixin = {
    methods: {
      getErrorMsg(err) {
        try {
          var errMsg = '';
          var data = err.response.data;
  
          if (typeof data === 'string') return data;
  
          data.forEach(element => {
            errMsg += element.message + '\n';
          });
          return errMsg;
        } catch (errMsg) {
          return err;
        }
      },
      getQueryVariable(variable) {
        const query = window.location.search.substring(1);
        const vars = query.split("&");
        for (let i = 0; i < vars.length; i++) {
          let pair = vars[i].split("=");
          if (decodeURIComponent(pair[0]) == variable) {
            return decodeURIComponent(pair[1]);
          }
        }
      }
    }
  };
  
  export default mixin;
  
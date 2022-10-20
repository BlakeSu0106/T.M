import request from "./request";

export const postCaptchaValidateAsync = (Captcha) =>
    request.post("/api/Captcha/validate", Captcha);

export const postCaptchaAsync = (phoneNumber) =>
    request.post("/api/Captcha/send", phoneNumber);
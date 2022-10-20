import request from "./request";

export const postValidatedAccountAsync = (accountInfo) =>
    request.post(`/api/Auth/validate/account`, accountInfo);
export const postUserRegisterAsync = (userInfo) =>
    request.post("/api/Auth/register", userInfo);
export const postLoginAsync = (account) =>
    request.post("/api/Auth/login", account);
export const postQuickLoginAsync = (account) =>
    request.post("/api/Auth/quick-login", account);
export const postRestPasswordAsync = (accountInfo) =>
    request.post("/api/Auth/password/forget", accountInfo);



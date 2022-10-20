import request from "./request";

export const getThirdPartyId = (companyId,type) =>
    request.get(`/api/Config?companyId=${companyId}&type=${type}`);

import request from "./request";

export const getRequiredAsync = (companyId,channelId) =>
    request.get(`/api/Profile/register?companyId=${companyId}&channelId=${channelId}`);

   
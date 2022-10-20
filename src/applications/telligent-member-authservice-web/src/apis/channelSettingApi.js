import request from "./request";

export const getChannelSettingAsync = (channelId) =>
  request.get("/api/ChannelSetting", { params: { channelId } });


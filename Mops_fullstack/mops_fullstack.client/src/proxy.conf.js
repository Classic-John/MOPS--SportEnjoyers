const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7225';

const PROXY_CONFIG = [
  {
    context: [
      "/api/player",
      "/api/group",
      "/api/field",
      "/api/match",
      "/api/thread",
      "/api/message"
    ],
    target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;

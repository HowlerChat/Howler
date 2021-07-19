import { getConfig } from "../config/config";

export const getServerToken = function(serverId: string, token: string) {
    // TODO: This and all token-related semantics need to be refactored to be decoupled from the
    // API so external auth providers can be used in non-web or alternative-web-hosted UIs.
    return () => fetch(`${getConfig().gatewayApiUrl}/oauth2/token`,
        {
          method: "POST",
          mode: "cors",
          headers:
          {
            'Content-Type': 'application/json',
            'Accept': 'application/json',
            'Authorization': 'Bearer ' + token
          },
          body: JSON.stringify({"serverId": serverId})}).then(response => {
            return response.json()
          });
};
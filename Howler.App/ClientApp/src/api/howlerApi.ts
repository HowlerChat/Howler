import { getConfig } from "../config/config";

export const getSpace = function(spaceId: string, token: string) {
    return () => fetch(getConfig().howlerApiUrl + `/v1/spaces/${spaceId}`,
            {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                }
            });
};
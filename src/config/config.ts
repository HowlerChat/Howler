import { getDevConfig } from './config.dev';

export const getConfig = function() {
    // TODO: switch on env
    return getDevConfig();
}
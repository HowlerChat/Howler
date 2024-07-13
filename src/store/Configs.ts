import {UserAuthorizers, UserData, UserServers, UserSpaces} from '../api/configApi';

export type ConfigState = { [userId: string]: UserAuthorizers & UserData & UserServers & UserSpaces };

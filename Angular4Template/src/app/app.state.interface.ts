import { AuthState } from './auth/reducers/auth.state';
import { UserState } from './user/reducers/user.state';

export interface AppState {
    auth: AuthState;
    users: UserState;
}

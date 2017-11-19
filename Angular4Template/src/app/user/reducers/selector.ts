import { AppState } from './../../app.state.interface';
import { createSelector } from 'reselect';
import { UserState } from './user.state';
import { List } from 'immutable';
import { Order } from '../../entities/order.entity';

// Base product state function
/**
 *
 *
 * @param {AppState} state
 * @returns {UserState}
 */
function getUserState(state: AppState): UserState {
    return state.users;
}

// ******************** Individual selectors ***************************
/**
 *
 *
 * @param {UserState} state
 * @returns {Order[]}
 */
const fetchUserOrders = function(state: UserState): Order[] {
    return state.orders.toJS();
};

// *************************** PUBLIC API's ****************************
export const getUserOrders = createSelector(getUserState, fetchUserOrders);

import { Map, Record, List } from 'immutable';
import { User } from '../../entities/user.entity';
import { Order } from '../../entities/order.entity';

/**
 *
 *
 * @export
 * @interface UserState
 * @extends {Map<string, any>}
 */
export interface UserState extends Map<string, any> {
  user: User;
  orders: List<Order[]>;
}

export const UserStateRecord = Record({
  user: Map({}),
  orders: List([])
});

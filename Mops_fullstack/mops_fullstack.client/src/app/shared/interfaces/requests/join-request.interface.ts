import { Group } from "../groups/group.interface";
import { Player } from "../players/player.interface";

export interface JoinRequest {
  player: Player,
  group: Group
}

import { Player } from "../players/player.interface";

export interface Group {
  id: Number;
  name: String;
  owner: Player;
  players?: Player[];
  isYours?: Boolean;
}

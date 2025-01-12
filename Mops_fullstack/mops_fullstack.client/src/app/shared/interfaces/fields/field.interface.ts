import { Player } from "../players/player.interface";

export interface Field {
  id: Number,
  name: String,
  owner: Player,
  location: String,
  reservedDates?: String[],
  isYours?: Boolean
}

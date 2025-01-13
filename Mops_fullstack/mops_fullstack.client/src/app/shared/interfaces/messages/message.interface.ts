import { Player } from "../players/player.interface";

export interface Message {
  id: Number,
  text: String,
  player: Player,
  dateCreated: Date,
  isYours: Boolean,
  isInitial: Boolean
}

import { Field } from "../fields/field.interface";
import { Group } from "../groups/group.interface";

export interface Match {
  id: Number,
  field: Field,
  group: Group,
  matchDate: String
}

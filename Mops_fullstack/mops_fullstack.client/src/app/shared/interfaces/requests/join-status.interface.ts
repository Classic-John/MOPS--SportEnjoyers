export enum GroupJoinStatusType {
  NoRequest = 0,
  Pending = 1,
  Joined = 2
}

export interface GroupJoinStatus {
  status: GroupJoinStatusType
}

import { ParamMap } from "@angular/router";

export class GroupFilter {
  name: String = "";
  owner: String = "";
  yours: Boolean = false;

  constructor(params?: ParamMap) {
    this.name = params ? (params.get('name') ?? "") : "";
    this.owner = params ? (params.get('owner') ?? "") : "";
    this.yours = (params?.get('yours') === 'true');
  }
}

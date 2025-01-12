import { ParamMap } from "@angular/router"

export class FieldFilter {
  name: String = ""
  owner: String = ""
  location: String = ""
  yours: Boolean = false
  freeOnDay: String = ""

  constructor(params?: ParamMap) {
    this.name = params ? (params.get('name') ?? "") : "";
    this.owner = params ? (params.get('owner') ?? "") : "";
    this.location = params ? (params.get('location') ?? "") : "";
    this.yours = (params?.get('yours') === 'true');
    this.freeOnDay = params ? (params.get('freeOnDay') ?? "") : "";
  }
}

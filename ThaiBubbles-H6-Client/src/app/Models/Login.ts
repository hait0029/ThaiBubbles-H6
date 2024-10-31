import { Role } from "./Role"

export interface Login {
"loginID": number
"email": string
"password": string
"roleId": number
roleType?: Role[]
}

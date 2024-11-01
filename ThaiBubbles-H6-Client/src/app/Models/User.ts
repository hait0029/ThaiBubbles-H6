import { City } from './City';
import { Login } from './Login';
export interface User {
  "userID": number
  "fName": string
  "lName": string
  "phoneNr": number
  "address": string
  "cityId": number
  "loginId": number
  Login?: Login[]
  City?: City[]
}

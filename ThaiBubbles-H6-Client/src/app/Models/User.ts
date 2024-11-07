import { City } from './City';
import { Login } from './Login';
import { Role } from './Role';
export interface User {
  "userID": number
  "email": string
  "password": string
  "fName": string
  "lName": string
  "phoneNr": number
  "address": string
  //favoriteId?: number;       // Optional property for FavoriteId
  "cityId": number
  "loginId": number
  Login?: Login[]
  City?: City[]
  role?: Role[]              // Role navigation property

}

import { City } from './City';
import { Role } from './Role';

export interface User {
  userID: number;
  email: string;
  password: string;
  fName: string;
  lName: string;
  phoneNr: string;
  address: string;
  cityId: number;  // Keep this as number as per your backend model
  roleID?: number; // Optional role ID, in case it is not assigned
  role?: Role;     // Role should be an object (not an array)
  city?: City;     // City should be an object (not an array)
}

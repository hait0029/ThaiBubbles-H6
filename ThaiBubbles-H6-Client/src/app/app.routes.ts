import { Routes } from '@angular/router';
import { HomeComponent } from './Frontpage/home/home.component';
import { MenuComponent } from './Menu/menu/menu.component';
import { LoginComponent } from './Login/login/login.component';
import { SignupComponent } from './SignUp/signup/signup.component';
import { ProductsComponent } from './Product/products/products.component';
import { AboutComponent } from './About/about/about.component';
import { AdminDashboardComponent } from './Admin/admin-dashboard/admin-dashboard.component'; // Import your admin component
import { adminGuard } from './Guards/admin.guard'; // Import the admin guard
import { UserProfileComponent } from './Profile/user-profile/user-profile.component';
import { SearchResultComponent } from './search-result/search-result.component';
import { UserCrudComponent } from './Admin/user-crud/user-crud.component';
import { ProductCrudComponent } from './Admin/product-crud/product-crud.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'menu', component: MenuComponent },
    { path: 'about', component: AboutComponent },
    { path: 'category/:categoryID', component: ProductsComponent },

    { path: 'search', component: SearchResultComponent },
    { path: 'login', component: LoginComponent },
    { path: 'signup', component: SignupComponent },

    // Admin Routes (Protected by adminGuard)
    { path: 'admin', component: AdminDashboardComponent, canActivate: [adminGuard] },
    {path: 'admin/user-crud', component: UserCrudComponent, canActivate: [adminGuard]},
    {path: 'admin/product-crud', component: ProductCrudComponent, canActivate: [adminGuard]},

    // Profile Route (accessible only to logged-in users)
    { path: 'profile', component: UserProfileComponent },
    // You can add more admin routes here, for example:
    // { path: 'admin/products', component: AdminProductsComponent, canActivate: [adminGuard] }
];

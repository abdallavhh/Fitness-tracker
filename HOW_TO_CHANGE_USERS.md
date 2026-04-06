# How to Change Users in the Dashboard

## Quick Steps

### Step 1: Click the Logout Button
- Look for the **⎋ Logout** button at the bottom of the left sidebar
- Click it

### Step 2: Confirm Logout
- A confirmation dialog will appear asking "Are you sure you want to logout?"
- Click **Yes** to confirm
- You will be returned to the login screen

### Step 3: Login with Different Credentials
- Enter the username and password for the user you want to switch to
- Click **Login** or press Enter

---

## Demo Credentials

The application includes the following demo accounts:

### User Account
- **Username:** `demo`
- **Password:** `password`
- **Access Level:** Regular user (sees Dashboard, Profile, Meals, Exercises, etc.)

### Admin Account
- **Username:** `admin`
- **Password:** `admin123`
- **Access Level:** Admin (sees Admin Dashboard, Manage Users, Reports, etc.)

---

## What Happens When You Logout

When you click logout and confirm:

1. ✅ Your session is cleared
2. ✅ All cached views are reset
3. ✅ You're redirected to the login screen
4. ✅ Your role-based access is cleared
5. ✅ The next login will determine what menu items you see

---

## What's Different Between User & Admin

### Regular User (demo/password)
- Dashboard with personal fitness stats
- Profile page
- Meals section
- Exercises log
- Health Metrics
- Personal Goals

### Admin User (admin/admin123)
- Admin Dashboard with system metrics
- Manage Users (view, edit, delete users)
- Admin Meals management
- Admin Exercises management
- Admin Goals management
- Admin Health Metrics
- Admin Reports

---

## Visual Guide

```
Dashboard (logged in)
        ↓
[Click ⎋ Logout button]
        ↓
[Confirm logout dialog]
        ↓
Login Screen
        ↓
[Enter different credentials]
        ↓
New Dashboard (as different user)
```

---

## Notes

- This is a demo application with hardcoded credentials
- In a production app, credentials would be validated against a database or authentication service
- You can add more demo accounts by modifying the `ValidateCredentials()` method in `LoginView.xaml.cs`
- The user's role (IsAdmin) is used to show/hide different sidebar menu items

---

## To Add More Demo Users

Edit the `ValidateCredentials()` method in `LoginView.xaml.cs`:

```csharp
private static bool ValidateCredentials(string username, string password)
{
    // Add your demo accounts here
    return (username == "demo" && password == "password") ||
           (username == "admin" && password == "admin123") ||
           (username == "john" && password == "john123");
}
```

And in `MainWindow.xaml.cs`, update the login handler to set `AppSession.IsAdmin`:

```csharp
private void LoginView_LoginSuccessful(object? sender, System.EventArgs e)
{
    // Set user info in AppSession
    AppSession.Username = username;
    AppSession.DisplayName = "John Doe"; // or get from database
    AppSession.IsAdmin = (username == "admin");

    // ... rest of code
}
```

[web] GET /api/ui/users/intercom-profile  (Dataverse.Api.Controllers.UI.Core.UsersController.IntercomProfile)  [L95–L123] [auth=Authentication.UserPolicy]
  └─ uses_service IntercomService
    └─ method GetContactByExternalId [L103]
  └─ uses_service UserService
    └─ method GetUserAsync [L98]


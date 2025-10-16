[web] GET /api/ui/users/intercom-profile  (Dataverse.Api.Controllers.UI.Core.UsersController.IntercomProfile)  [L95–L123] status=200 [auth=Authentication.UserPolicy]
  └─ uses_service IntercomService
    └─ method GetContactByExternalId [L103]
      └─ implementation Dataverse.Services.ModuleIntegrations.Services.IntercomService.GetContactByExternalId [L17-L124]
  └─ uses_service UserService
    └─ method GetUserAsync [L98]
      └─ implementation Dataverse.ApplicationService.Services.UserService.GetUserAsync [L15-L185]


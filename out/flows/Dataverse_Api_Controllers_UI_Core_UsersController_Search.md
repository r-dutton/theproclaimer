[web] GET /api/ui/users  (Dataverse.Api.Controllers.UI.Core.UsersController.Search)  [L125–L167] [auth=Authentication.UserPolicy]
  └─ sends_request FindUsersLightQuery [L164]
  └─ sends_request FindUsersUltraLightQuery [L154]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.FindSingleTenantCentralUsersQueryHandler.Handle [L85–L194]
      └─ uses_service IControlledRepository<User>
        └─ method ReadQuery [L125]
  └─ sends_request FindUsersUltraLightWithStandardHoursQuery [L160]
  └─ sends_request FindUsersWithLicensesQuery [L148]


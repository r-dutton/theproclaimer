[web] GET /api/ui/users  (Dataverse.Api.Controllers.UI.Core.UsersController.Search)  [L125–L167] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request FindUsersLightQuery [L164]
  └─ sends_request FindUsersUltraLightWithStandardHoursQuery [L160]
  └─ sends_request FindUsersUltraLightQuery -> FindSingleTenantCentralUsersQueryHandler [L154]
    └─ handled_by Dataverse.ApplicationService.Queries.Firms.Users.FindSingleTenantCentralUsersQueryHandler.Handle [L85–L194]
      └─ calls UserRepository.ReadQuery [L125]
  └─ sends_request FindUsersWithLicensesQuery [L148]
  └─ impact_summary
    └─ requests 4
      └─ FindUsersLightQuery
      └─ FindUsersUltraLightQuery
      └─ FindUsersUltraLightWithStandardHoursQuery
      └─ FindUsersWithLicensesQuery
    └─ handlers 1
      └─ FindSingleTenantCentralUsersQueryHandler


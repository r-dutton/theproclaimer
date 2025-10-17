[web] POST /api/imanage/authorisation-url  (DataGet.Api.Controllers.Integrations.IManageController.GetAuthorisationUrl)  [L50–L65] status=201 [auth=Authentication.MachineToMachinePolicy]
  └─ uses_service IConnectionService (InstancePerLifetimeScope)
    └─ method GetCurrentUsersConnection [L53]
      └─ implementation Workpapers.Next.Services.ConnectionService.GetCurrentUsersConnection [L24-L211]
        └─ uses_service ApiService (SingleInstance)
          └─ method GetAccountingFiles [L81]
            └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetAccountingFiles [L14-L75]
  └─ sends_request CreateOrUpdateIntegrationConfigCommand -> CreateOrUpdateIntegrationConfigCommandHandler [L58]
    └─ handled_by DataGet.Connections.App.Commands.CreateOrUpdateIntegrationConfigCommandHandler.Handle [L25–L52]
      └─ uses_service IControlledRepository<IntegrationConfiguration> (Scoped (inferred))
        └─ method WriteQuery [L36]
          └─ implementation DataGet.Data.Repository.Connections.IntegrationConfigRepository.WriteQuery [L5-L35]
  └─ sends_request GetIManageAuthorizationUrlQuery -> GetAuthorizationUrlQueryHandler [L55]
    └─ handled_by DataGet.Integrations.iManage.Api.Queries.GetAuthorizationUrlQueryHandler.Handle [L25–L41]
      └─ uses_service IManageService
        └─ method GetAuthorisationUrl [L38]
          └─ implementation DataGet.Integrations.iManage.Api.Services.IManageService.GetAuthorisationUrl [L12-L223]
            └─ uses_client IManageApiClient [L33]
            └─ uses_service RequestProcessor
              └─ method GetAuthorisationUrl [L28]
                └─ resolves_request DataGet.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                └─ resolves_request Cirrus.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                └─ resolves_request Dataverse.Services.Features.Requests.RequestProcessor.GetAuthorisationUrl
                └─ +1 additional_requests elided
  └─ impact_summary
    └─ clients 1
      └─ IManageApiClient
    └─ services 1
      └─ IConnectionService
    └─ requests 2
      └─ CreateOrUpdateIntegrationConfigCommand
      └─ GetIManageAuthorizationUrlQuery
    └─ handlers 2
      └─ CreateOrUpdateIntegrationConfigCommandHandler
      └─ GetAuthorizationUrlQueryHandler


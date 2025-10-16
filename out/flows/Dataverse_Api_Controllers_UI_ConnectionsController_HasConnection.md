[web] GET /api/ui/connections/has-connection  (Dataverse.Api.Controllers.UI.ConnectionsController.HasConnection)  [L42–L58] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetConnectionSummaryQuery [L47]
    └─ handled_by Cirrus.Connections.DataGet.Queries.GetConnectionSummaryQueryHandler.Handle [L22–L93]
      └─ maps_to ActiveConnectionDto [L55]
        └─ automapper.registration CirrusMappingProfile (SourceType->ActiveConnectionDto) [L208]
        └─ automapper.registration WorkpapersMappingProfile (SourceType->ActiveConnectionDto) [L86]
      └─ uses_client DataGetClient [L64]
      └─ uses_service ApiService (SingleInstance)
        └─ method GetFeatures [L69]
          └─ implementation Cirrus.ApplicationService.Accounting.Services.ApiService.GetFeatures [L14-L75]
      └─ uses_service UserService
        └─ method GetUserId [L54]
          └─ implementation Cirrus.ApplicationService.Services.UserService.GetUserId [L14-L122]
      └─ uses_service DataGetClient
        └─ method GetConnectionsAsync [L64]
          └─ implementation Cirrus.Connections.DataGet.Client.DataGetClient.GetConnectionsAsync [L15-L302]
      └─ uses_service IControlledRepository<FileToken>
        └─ method ReadQuery [L61]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<SourceType>
        └─ method ReadQuery [L55]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<UserToken>
        └─ method ReadQuery [L62]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L57]
          └─ ... (no implementation details available)


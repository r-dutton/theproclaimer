[web] GET /api/ui/account-mapping/mapping-codes  (Dataverse.Api.Controllers.UI.AccountMapping.MappingCodesController.GetMappingCodes)  [L59–L68] status=200 [auth=Authentication.UserPolicy]
  └─ sends_request GetMappingCodesQuery [L67]
    └─ handled_by Dataverse.ApplicationService.Queries.AccountMapping.MappingCodes.GetMappingCodesQueryHandler.Handle [L36–L107]
      └─ maps_to MappingCodeDto [L89]
      └─ maps_to MappingCodeDto [L76]
      └─ uses_service UserService
        └─ method IsMaster [L58]
          └─ implementation Dataverse.ApplicationService.Services.UserService.IsMaster [L15-L185]
      └─ uses_service IControlledRepository<ExcludedMappingCode>
        └─ method ReadQuery [L65]
          └─ ... (no implementation details available)
      └─ uses_service IControlledRepository<MappingCode>
        └─ method ReadQuery [L53]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L79]
          └─ ... (no implementation details available)


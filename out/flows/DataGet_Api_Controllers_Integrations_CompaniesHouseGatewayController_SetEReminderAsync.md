[web] PUT /api/companies-house-gateway/e-reminder  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.SetEReminderAsync)  [L111–L115] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetEReminderQuery [L114]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetEReminderQueryHandler.Handle [L33–L61]
      └─ maps_to ERemindersResponseDto [L59]
      └─ uses_client CompaniesHouseInputGatewayClient [L55]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L55]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method Create [L54]
      └─ uses_service IMapper
        └─ method Map [L59]


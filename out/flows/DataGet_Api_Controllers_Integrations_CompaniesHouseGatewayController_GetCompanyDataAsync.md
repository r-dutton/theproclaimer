[web] GET /api/companies-house-gateway/company-info  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetCompanyDataAsync)  [L27–L31] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyDataQuery [L30]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetCompanyDataQueryHandler.Handle [L35–L64]
      └─ maps_to CompanyDataDto [L62]
      └─ uses_client CompaniesHouseInputGatewayClient [L58]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L58]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method Create [L57]
      └─ uses_service IMapper
        └─ method Map [L62]


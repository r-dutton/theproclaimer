[web] GET /api/companies-house-gateway/company-info  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetCompanyDataAsync)  [L27–L31] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetCompanyDataQuery -> GetCompanyDataQueryHandler [L30]
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetCompanyDataQueryHandler.Handle [L35–L64]
      └─ maps_to CompanyDataDto [L62]
      └─ uses_client CompaniesHouseInputGatewayClient [L58]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L58]
          └─ implementation DataGet.Integrations.CompaniesHouseGateway.Api.Gateway.CompaniesHouseInputGatewayClient.SendAsync [L13-L227]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method Create [L57]
          └─ ... (no implementation details available)
  └─ impact_summary
    └─ clients 1
      └─ CompaniesHouseInputGatewayClient
    └─ requests 1
      └─ GetCompanyDataQuery
    └─ handlers 1
      └─ GetCompanyDataQueryHandler
    └─ mappings 1
      └─ CompanyDataDto


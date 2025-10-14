[web] GET /api/companies-house-gateway/payment-periods  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetPaymentPeriodsAsync)  [L48–L52] [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetPaymentPeriodsQuery [L51]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetPaymentPeriodsQueryHandler.Handle [L33–L61]
      └─ maps_to PaymentPeriodsResponseDto [L59]
      └─ uses_client CompaniesHouseInputGatewayClient [L55]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L55]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method Create [L54]
      └─ uses_service IMapper
        └─ method Map [L59]


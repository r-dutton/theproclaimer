[web] GET /api/companies-house-gateway/payment-periods  (DataGet.Api.Controllers.Integrations.CompaniesHouseGatewayController.GetPaymentPeriodsAsync)  [L48–L52] status=200 [auth=Authentication.MachineToMachinePolicy]
  └─ sends_request GetPaymentPeriodsQuery [L51]
    └─ handled_by DataGet.Integrations.CompaniesHouseGateway.Api.Queries.GetPaymentPeriodsQueryHandler.Handle [L33–L61]
      └─ maps_to PaymentPeriodsResponseDto [L59]
      └─ uses_client CompaniesHouseInputGatewayClient [L55]
      └─ uses_service CompaniesHouseInputGatewayClient
        └─ method SendAsync [L55]
          └─ implementation DataGet.Integrations.CompaniesHouseGateway.Api.Gateway.CompaniesHouseInputGatewayClient.SendAsync [L13-L227]
      └─ uses_service GovTalkEnvelopeCreator
        └─ method Create [L54]
          └─ ... (no implementation details available)
      └─ uses_service IMapper
        └─ method Map [L59]
          └─ ... (no implementation details available)


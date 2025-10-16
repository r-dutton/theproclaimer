[web] GET /api/dataverse/subscription-info  (Workpapers.Next.API.Controllers.DataverseController.GetSubscriptionCreateInfo)  [L187–L192] status=200 [auth=AuthorizationPolicies.M2M]
  └─ sends_request GetSubscriptionInfoQuery [L191]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Dataverse.GetSubscriptionInfoQueryHandler.Handle [L33–L83]
      └─ maps_to ProductSetDto [L61]
      └─ maps_to StarterPackDto [L66]
      └─ maps_to WholesalerDto [L70]
      └─ maps_to TemplateStandardAccountSetDto [L48]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L52]
          └─ ... (no implementation details available)
      └─ uses_service UnitOfWork
        └─ method Table [L48]
          └─ ... (no implementation details available)


[web] GET /api/dataverse/subscription-info  (Workpapers.Next.API.Controllers.DataverseController.GetSubscriptionCreateInfo)  [L186–L191] [auth=AuthorizationPolicies.M2M]
  └─ sends_request GetSubscriptionInfoQuery [L190]
    └─ generic_pipeline_behaviors 2
      └─ DatagetTokenSyncBehaviour
      └─ DatagetTokenSyncBehaviour
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Dataverse.GetSubscriptionInfoQueryHandler.Handle [L33–L83]
      └─ maps_to ProductSetDto [L61]
      └─ maps_to StarterPackDto [L66]
      └─ maps_to WholesalerDto [L70]
      └─ maps_to TemplateStandardAccountSetDto [L48]
      └─ uses_service IMapper
        └─ method ConfigurationProvider [L52]
      └─ uses_service UnitOfWork
        └─ method Table [L48]


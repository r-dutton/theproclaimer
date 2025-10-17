[web] GET /api/dataverse/subscription-info  (Workpapers.Next.API.Controllers.DataverseController.GetSubscriptionCreateInfo)  [L187–L192] status=200 [auth=AuthorizationPolicies.M2M]
  └─ sends_request GetSubscriptionInfoQuery -> GetSubscriptionInfoQueryHandler [L191]
    └─ handled_by Workpapers.Next.ApplicationService.Queries.Dataverse.GetSubscriptionInfoQueryHandler.Handle [L33–L83]
      └─ maps_to WholesalerDto [L70]
      └─ maps_to StarterPackDto [L66]
      └─ maps_to ProductSetDto [L61]
      └─ maps_to TemplateStandardAccountSetDto [L48]
      └─ uses_service UnitOfWork
        └─ method Table [L48]
          └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ requests 1
      └─ GetSubscriptionInfoQuery
    └─ handlers 1
      └─ GetSubscriptionInfoQueryHandler
    └─ mappings 4
      └─ ProductSetDto
      └─ StarterPackDto
      └─ TemplateStandardAccountSetDto
      └─ WholesalerDto


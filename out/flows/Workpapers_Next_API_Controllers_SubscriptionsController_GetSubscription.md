[web] GET /api/subscriptions/{id:int}  (Workpapers.Next.API.Controllers.SubscriptionsController.GetSubscription)  [L38–L48] status=200 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to SubscriptionDto [L41]
  └─ uses_service UnitOfWork
    └─ method Table [L41]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ SubscriptionDto


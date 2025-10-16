[web] POST /api/subscriptions  (Workpapers.Next.API.Controllers.SubscriptionsController.PostSubscription)  [L61–L72] status=201 [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to SubscriptionDto [L71]
  └─ uses_service UnitOfWork
    └─ method Table [L64]
      └─ implementation UnitOfWork.Table
  └─ impact_summary
    └─ services 1
      └─ UnitOfWork
    └─ mappings 1
      └─ SubscriptionDto


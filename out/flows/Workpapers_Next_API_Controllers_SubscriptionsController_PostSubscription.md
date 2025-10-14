[web] POST /api/subscriptions  (Workpapers.Next.API.Controllers.SubscriptionsController.PostSubscription)  [L61–L72] [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to SubscriptionDto [L71]
  └─ uses_service IMapper
    └─ method Map [L71]
  └─ uses_service UnitOfWork
    └─ method Table [L64]


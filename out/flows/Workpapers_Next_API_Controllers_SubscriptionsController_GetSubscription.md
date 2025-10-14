[web] GET /api/subscriptions/{id:int}  (Workpapers.Next.API.Controllers.SubscriptionsController.GetSubscription)  [L38–L48] [auth=AuthorizationPolicies.SuperUser]
  └─ maps_to SubscriptionDto [L41]
  └─ uses_service UnitOfWork
    └─ method Table [L41]


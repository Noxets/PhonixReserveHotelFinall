using Hamkare.Infrastructure;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace Hamkare.Component;

public class ComponentRoot : ComponentBase, IDisposable
{
    [Inject] private PersistentComponentState ApplicationState { get; set; }

    public ApplicationDbContext Context { get; set; }

    [Inject] public IDbContextFactory<ApplicationDbContext> DbContextFactory { get; set; }

    [Parameter] public bool RenderEnded { get; set; }

    [Parameter] public bool AfterFirstRender { get; set; }

    private readonly IList<PersistingComponentStateSubscription> _subscriptions =
        new List<PersistingComponentStateSubscription>();

    protected async Task<TResult> GetOrAddState<TResult>(string key, Func<Task<TResult>> addState)
    {
        TResult data = default;

        if (ApplicationState.TryTakeFromJson(key, out data))
            return data;

        data = await addState.Invoke();
        _subscriptions.Add(ApplicationState.RegisterOnPersisting(() =>
            {
                ApplicationState.PersistAsJson(key, data);
                return Task.CompletedTask;
            })
        );
        
        return data;
    }

    public void Dispose()
    {
        Context?.Dispose();

        if (!_subscriptions.Any())
            return;

        foreach (var item in _subscriptions)
            item.Dispose();
    }

    protected async Task GetContextAsync()
    {
        var context = await DbContextFactory.CreateDbContextAsync();
        Context = context;
    }
}

// forecast = await GetOrAdd(key, async () =>{}
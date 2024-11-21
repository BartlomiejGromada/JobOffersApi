using System.Threading;
using JobOffersApi.Abstractions.Contexts;

namespace JobOffersApi.Infrastructure.Contexts;

public sealed class ContextAccessor
{
    private static readonly AsyncLocal<ContextHolder> Holder = new();

    public IContext Context
    {
        get => Holder.Value?.Context;
        set
        {
            var holder = Holder.Value;
            if (holder != null)
            {
                holder.Context = null;
            }

            if (value != null)
            {
                Holder.Value = new ContextHolder {Context = value};
            }
        }
    }

    private class ContextHolder
    {
        public IContext Context;
    }
}
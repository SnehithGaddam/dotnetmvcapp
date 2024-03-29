﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Collections.Immutable;
using System.ComponentModel.Composition;

using Microsoft.VisualStudio.Imaging.Interop;
using Microsoft.VisualStudio.ProjectSystem.VS.Tree.Dependencies.CrossTarget;
using Microsoft.VisualStudio.ProjectSystem.VS.Tree.Dependencies.Models;

namespace Microsoft.VisualStudio.ProjectSystem.VS.Tree.Dependencies.Subscriptions.RuleHandlers
{
    [Export(DependencyRulesSubscriber.DependencyRulesSubscriberContract, typeof(IDependenciesRuleHandler))]
    [Export(typeof(IProjectDependenciesSubTreeProvider))]
    [AppliesTo(ProjectCapability.DependenciesTree)]
    internal sealed class FrameworkRuleHandler : DependenciesRuleHandlerBase
    {
        public const string ProviderTypeString = "FrameworkDependency";

        private static readonly DependencyIconSet s_iconSet = new DependencyIconSet(
            icon: ManagedImageMonikers.Framework,
            expandedIcon: ManagedImageMonikers.Framework,
            unresolvedIcon: ManagedImageMonikers.FrameworkWarning,
            unresolvedExpandedIcon: ManagedImageMonikers.FrameworkWarning);

        private static readonly SubTreeRootDependencyModel s_rootModel = new SubTreeRootDependencyModel(
            ProviderTypeString,
            Resources.FrameworkNodeName,
            s_iconSet,
            DependencyTreeFlags.FrameworkSubTreeRootNode);

        public FrameworkRuleHandler()
            : base(FrameworkReference.SchemaName, ResolvedFrameworkReference.SchemaName)
        {
        }

        public override string ProviderType => ProviderTypeString;

        public override IDependencyModel CreateRootDependencyNode() => s_rootModel;

        protected override IDependencyModel CreateDependencyModel(
            string path,
            string originalItemSpec,
            bool resolved,
            bool isImplicit,
            IImmutableDictionary<string, string> properties)
        {
            return new FrameworkDependencyModel(
                path,
                originalItemSpec,
                resolved,
                properties);
        }

        public override ImageMoniker GetImplicitIcon() => ManagedImageMonikers.FrameworkPrivate;
    }
}

using System;
using System.Collections.Generic;

public class JBlockComparer : IComparer<JBlock>
{
    public int Compare(JBlock x, JBlock y)
    {
        // Compare by domain name
        int domainComparison = String.Compare(x.domain, y.domain, StringComparison.OrdinalIgnoreCase);
        if (domainComparison != 0)
            return domainComparison;

        // Compare by cluster name
        int clusterComparison = String.Compare(x.cluster, y.cluster, StringComparison.OrdinalIgnoreCase);
        if (clusterComparison != 0)
            return clusterComparison;

        // Compare by standardid
        return String.Compare(x.standardid, y.standardid, StringComparison.OrdinalIgnoreCase);
    }
}
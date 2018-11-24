export class StringUtils {
    public static isNullOrEmpty(s: string) {
        return !s || s.length === 0;
    }
}